export class Serializable {

    fromJSON(json: any) {
        for (const propName in Object.keys(json)) {
            if (json.hasOwnProperty(propName)) {
                this[propName] = json[propName];
            }
        }
        return this;
    }
}
