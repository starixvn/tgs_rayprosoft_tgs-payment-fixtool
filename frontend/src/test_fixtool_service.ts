import { XhrFactory } from '@angular/common';
import { HttpClient, HttpXhrBackend } from '@angular/common/http';
import { FixToolService } from './app/pages/services/fix-tool.service';

class TestFactory implements XhrFactory {
  build(): XMLHttpRequest {
    return new XMLHttpRequest();
  }
}
let httpClient = new HttpClient(new HttpXhrBackend(new TestFactory));

let svc = new FixToolService(httpClient);

svc.listBrandMaps().forEach(v => console.log(v));
