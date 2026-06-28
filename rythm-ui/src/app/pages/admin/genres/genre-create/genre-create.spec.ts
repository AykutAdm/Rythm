import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenreCreate } from './genre-create';

describe('GenreCreate', () => {
  let component: GenreCreate;
  let fixture: ComponentFixture<GenreCreate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenreCreate],
    }).compileComponents();

    fixture = TestBed.createComponent(GenreCreate);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
