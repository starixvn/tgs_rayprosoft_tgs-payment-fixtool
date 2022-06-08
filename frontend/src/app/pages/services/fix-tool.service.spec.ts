import { TestBed } from '@angular/core/testing';

import { FixToolService } from './fix-tool.service';

describe('FixToolService', () => {
  let service: FixToolService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FixToolService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
