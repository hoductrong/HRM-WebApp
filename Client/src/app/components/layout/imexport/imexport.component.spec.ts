import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ImexportComponent } from './imexport.component';

describe('ImexportComponent', () => {
  let component: ImexportComponent;
  let fixture: ComponentFixture<ImexportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ImexportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ImexportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
