import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenreUpdate } from './genre-update';

describe('GenreUpdate', () => {
  let component: GenreUpdate;
  let fixture: ComponentFixture<GenreUpdate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenreUpdate],
    }).compileComponents();

    fixture = TestBed.createComponent(GenreUpdate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
