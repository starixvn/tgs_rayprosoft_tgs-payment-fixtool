"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var FixToolService = /** @class */ (function () {
    function FixToolService(http) {
        this.http = http;
    }
    FixToolService.prototype.listWrongWallets = function () {
        return this.http
            .get("/api/wallet")
            .then(function (res) { return res.data; });
    };
    FixToolService.prototype.getWrongWallet = function (walletId) {
        return this.http
            .get("/api/wallet/".concat(walletId))
            .then(function (res) { return res.data; });
    };
    FixToolService.prototype.listPlayerWithdrawForms = function (brandId, playerCode) {
        return this.http
            .get("/api/withdraw/forms/".concat(brandId, "/").concat(playerCode))
            .then(function (res) { return res.data; });
    };
    FixToolService.prototype.listTransactions = function (walletId) {
        return this.http
            .get("/api/transaction/".concat(walletId))
            .then(function (res) { return res.data; });
    };
    FixToolService.prototype.fixWithdrawPaid = function (withdrawFormId, walletId) {
        return this.http
            .post("/api/withdraw/paid/".concat(withdrawFormId, "/").concat(walletId), null)
            .then(function (res) { return res.data; });
    };
    FixToolService.prototype.fixWithdrawReject = function (withdrawFormId, walletId) {
        return this.http
            .post("/api/withdraw/reject/".concat(withdrawFormId, "/").concat(walletId), null)
            .then(function (res) { return res.data; });
    };
    FixToolService.prototype.listBrandMaps = function () {
        return this.http
            .get('/api/brand')
            .then(function (res) { return res.data; });
    };
    return FixToolService;
}());
exports.default = FixToolService;
