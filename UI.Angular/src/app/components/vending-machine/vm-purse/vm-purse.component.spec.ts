import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VMPurseComponent } from './vm-purse.component';

describe('VMPurseComponent', () => {
  let component: VMPurseComponent;
  let fixture: ComponentFixture<VMPurseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VMPurseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VMPurseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
