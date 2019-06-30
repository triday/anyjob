require("globalmodule");
require("packmodule");
exports.run = function (num1 = 1, num2 = 3) {
    console.log("begin add");
    return num1+num2;
};