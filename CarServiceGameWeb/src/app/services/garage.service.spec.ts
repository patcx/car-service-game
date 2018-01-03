import { TestBed, inject } from '@angular/core/testing';

import { GarageService } from './garage.service';

describe('GarageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GarageService]
    });
  });

  it('should be created', inject([GarageService], (service: GarageService) => {
    expect(service).toBeTruthy();
  }));
});
