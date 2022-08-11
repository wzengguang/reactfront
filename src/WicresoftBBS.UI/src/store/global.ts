import { makeAutoObservable } from 'mobx';

class GlobalData {

    lang = 'zh-cn';

    constructor() {
        makeAutoObservable(this);
    }
}

export { GlobalData };
export default new GlobalData();