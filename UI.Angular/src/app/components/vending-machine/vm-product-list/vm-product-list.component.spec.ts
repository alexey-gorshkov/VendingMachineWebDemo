import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VMProductListComponent } from './vm-product-list.component';

describe('VMProductListComponent', () => {
  let component: VMProductListComponent;
  let fixture: ComponentFixture<VMProductListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VMProductListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VMProductListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
