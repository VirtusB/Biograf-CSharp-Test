/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MemberDiscountStepComponent } from './member-discount-step.component';

describe('MemberDiscountStepComponent', () => {
  let component: MemberDiscountStepComponent;
  let fixture: ComponentFixture<MemberDiscountStepComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MemberDiscountStepComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MemberDiscountStepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
