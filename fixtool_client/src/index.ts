// import FixToolService from './services/fix-tool-service.js';
// import axiosConfigFactory from './models/axios-configuration.js';

// let instance = axiosConfigFactory("test-core");

// let svc = new FixToolService(instance);

// var result = await svc.listWrongWallets();

// console.log(result);
const axiosConfigFactory = require('./models/axios-configuration.js').default;
const FixToolService = require('./services/fix-tool-service.js').default;

let instance = axiosConfigFactory("test-core");

let svc = new FixToolService(instance);

svc.listWrongWallets().then((ret: any) => console.log(ret));
