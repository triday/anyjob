(function () {
    var RESULT_SPLIT_TEXT = "***[[[!@#$%^&*()_!@#$%^&*(!@#$%^&]]]***";

    var entry = process.argv[2];
    var argsType = process.argv[3];
    var args = JSON.parse(process.argv[4]);

    try {
        var module = require(entry);
        var fun = module.run;
        if(typeof fun !== "function"){
            throw "Can not find run function.";
        }
        var fun_args_define = parseFuncArguments(module.run);
        var fun_args = fun_args_define.map(function (p) { return args[p]; });
        var result = fun.apply(this, fun_args);
        console.log(RESULT_SPLIT_TEXT);
        console.log(JSON.stringify({ 'result': result }));
    } catch (error) {
        var errorInfo = {
            message: error.message,
            type: error.name,
            stack: error.stack
        };
        console.log(RESULT_SPLIT_TEXT);
        console.log(JSON.stringify({ 'error': errorInfo }));
    }
    /**
     * 
     * @param {Function} fun 
     */
    function parseFuncArguments(fun) {
        var define = fun.toString();
        var startIndex = define.indexOf('(');
        var endIndex = define.indexOf(')', startIndex + 1);
        var argsLine = define.substring(startIndex + 1, endIndex).trim();
        if (!argsLine) return [];
        return argsLine.split(',').map(parseArgument);
    }
    /**
     * 
     * @param {String} argdefine 
     */
    function parseArgument(argdefine) {
        var index = argdefine.indexOf('=');
        return index > 0 ? argdefine.substring(0, index).trim() : argdefine.trim();
    }
})();
