"use strict";
class classname_process {
    constructor(...args) {
        this.classes = [];
        this.collapse(...args);
        this.result = this.classes.join(' ');
    }
    collapse(...args) {
        for (var i = 0; i < args.length; i++) {
            var arg = args[i];
            if (!arg)
                continue;
            var argType = typeof arg;
            if (argType === 'string' || argType === 'number') {
                this.classes.push(arg);
            }
            else if (Array.isArray(arg)) {
                this.collapse(...arg);
            }
            else if (argType === 'object') {
                for (var key in arg) {
                    if (arg[key]) {
                        this.classes.push(key);
                    }
                }
            }
        }
    }
}
function classNames(...args) {
    return (new classname_process(...args)).result;
}
exports.classNames = classNames;
//# sourceMappingURL=classnames.js.map