import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArtistUpdate } from './artist-update';

describe('ArtistUpdate', () => {
  let component: ArtistUpdate;
  let fixture: ComponentFixture<ArtistUpdate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArtistUpdate],
    }).compileComponents();

    fixture = TestBed.createComponent(ArtistUpdate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
