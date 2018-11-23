/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed, inject, } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HomeComponent } from './home.component';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>; // test environment for this component
  let de: DebugElement; // rendered html

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


});


describe('Testing async functions', () => {
  it('should work with async/await', async () => {
    // Arrange
    let flag = false;

    // Act
    flag = await returnTrueAsync();

    // Assert
    expect(flag).toBeTruthy();
  });
});


// just a test function
// it returns true as a promise
function returnTrueAsync() {
  return new Promise(resolve => {
    setTimeout(() => {
      resolve(true);
    }, 1000);
  });
}
