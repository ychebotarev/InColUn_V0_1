export function isPresent(obj: any): boolean {
  return obj !== undefined && obj !== null;
}
export function isString(obj: any): boolean {
  return typeof obj === "string";
}

export function isFunction(obj: any): boolean {
  return typeof obj === "function";
}

export function isType(obj: any): boolean {
  return isFunction(obj);
}

export function isArray(obj: any): boolean {
  return Array.isArray(obj);
}

export function isNumber(obj): boolean {
  return typeof obj === 'number';
}

export function isBlank(obj: any): boolean {
  return obj === undefined || obj === null;
}


export function urljoin(...args:any[]):string {
    var joined:string = [].slice.call(arguments, 0).join('/');
    // make sure protocol is followed by two slashes
    joined = joined.replace(/:\//g, '://');

    // remove consecutive slashes
    joined = joined.replace(/([^:\s])\/+/g, '$1/');

    // remove trailing slash before parameters or hash
    joined = joined.replace(/\/(\?|#)/g, '$1');

    return joined;
}

