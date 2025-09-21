import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IconGrid } from './icon-grid';

describe('IconGrid', () => {
  let component: IconGrid;
  let fixture: ComponentFixture<IconGrid>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IconGrid]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IconGrid);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
