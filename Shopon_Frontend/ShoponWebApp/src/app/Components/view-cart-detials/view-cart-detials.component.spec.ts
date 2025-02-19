import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewCartDetialsComponent } from './view-cart-detials.component';

describe('ViewCartDetialsComponent', () => {
  let component: ViewCartDetialsComponent;
  let fixture: ComponentFixture<ViewCartDetialsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewCartDetialsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewCartDetialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
