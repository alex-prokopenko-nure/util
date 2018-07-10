"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function ValidatePasswords(control, pass) {
    if (!(control.value == pass)) {
        return { validPassword: true };
    }
    return null;
}
exports.ValidatePasswords = ValidatePasswords;
//# sourceMappingURL=equality.validator.js.map