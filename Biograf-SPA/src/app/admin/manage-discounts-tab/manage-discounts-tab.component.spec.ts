/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ManageDiscountsTabComponent } from './manage-discounts-tab.component';

describe('ManageDiscountsTabComponent', () => {
  let component: ManageDiscountsTabComponent;
  let fixture: ComponentFixture<ManageDiscountsTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageDiscountsTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageDiscountsTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
