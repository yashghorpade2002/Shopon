# audio_recognizer.py
import http.server
import socketserver
import json
import os
from urllib.parse import urlparse
from io import BytesIO
import speech_recognition as sr

class Handler(http.server.SimpleHTTPRequestHandler):
    def do_POST(self):
        if self.path == '/recognize':
            content_length = int(self.headers['Content-Length'])
            audio_data = self.rfile.read(content_length)
            
            # Save the received audio data to a temporary file
            with open('temp_audio.wav', 'wb') as f:
                f.write(audio_data)

            # Recognize the speech in the audio file
            recognizer = sr.Recognizer()
            with sr.AudioFile('temp_audio.wav') as source:
                audio = recognizer.record(source)  # read the entire audio file

            try:
                text = recognizer.recognize_google(audio)
                response = {'text': text}
                self.send_response(200)
                self.send_header('Content-Type', 'application/json')
                self.end_headers()
                self.wfile.write(json.dumps(response).encode())
            except sr.UnknownValueError:
                self.send_error(400, "Could not understand audio")
            except sr.RequestError as e:
                self.send_error(500, f"Could not request results; {e}")

# Set up and start the server
PORT = 8000
with socketserver.TCPServer(("", PORT), Handler) as httpd:
    print(f"Serving on port {PORT}")
    httpd.serve_forever()
