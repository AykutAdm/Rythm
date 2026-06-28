import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumUpdate } from './album-update';

describe('AlbumUpdate', () => {
  let component: AlbumUpdate;
  let fixture: ComponentFixture<AlbumUpdate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AlbumUpdate],
    }).compileComponents();

    fixture = TestBed.createComponent(AlbumUpdate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
