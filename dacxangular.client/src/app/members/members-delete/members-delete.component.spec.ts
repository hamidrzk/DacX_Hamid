import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembersDeleteComponent } from './members-delete.component';

describe('MembersDeleteComponent', () => {
  let component: MembersDeleteComponent;
  let fixture: ComponentFixture<MembersDeleteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MembersDeleteComponent]
    });
    fixture = TestBed.createComponent(MembersDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
