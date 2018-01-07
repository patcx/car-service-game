import { TestBed, inject } from '@angular/core/testing';

import { StallService } from './stall.service';

describe('StallService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StallService]
    });
  });

  it('should be created', inject([StallService], (service: StallService) => {
    expect(service).toBeTruthy();
  }));
});
