import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SongUpdate } from './song-update';

describe('SongUpdate', () => {
  let component: SongUpdate;
  let fixture: ComponentFixture<SongUpdate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SongUpdate],
    }).compileComponents();

    fixture = TestBed.createComponent(SongUpdate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
