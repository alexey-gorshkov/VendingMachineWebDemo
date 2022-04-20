
interface IEnvironmentSetting {
    apiUrl: { [index: string]: string };
    publicApiUrl: { [index: string]: string };
    user?: { [index: string]: any };
}
const devApiUrls = {
    auth: 'https://localhost:5001/api/auth/',
    payment: 'https://localhost:5001/api/payment/',
    vendingMachine: 'https://localhost:5001/api/vending-machine/'
};
const devSettings: IEnvironmentSetting = {
    apiUrl: devApiUrls,
    publicApiUrl: devApiUrls,
    user: {
        userName: 'admin',
        userRoles: [
            'senior_adm'
        ]
    }
};

const prodSettings: IEnvironmentSetting = {
    publicApiUrl: {        
        auth: 'https://localhost:5001/api/auth/'
    },
    apiUrl: {
        payment: 'https://localhost:5001/api/payment/',
        vendingMachine: 'https://localhost:5001/api/vending-machine/'
    }
};

class Config {
    private static instance: Config;
    private _settings: IEnvironmentSetting;
    static getInstance = () => {
        if (!Config.instance) {
            Config.instance = new Config();
        }
        return Config.instance;
    };
    private constructor() {
        this._settings = process.env.ENV_CONFIGURATION === 'prod' ? prodSettings : devSettings;
    }

    get settings() {
        return this._settings;
    }

    get apiUrl() {
        return this._settings.apiUrl;
    }
    get publicApiUrl() {
        return this._settings.publicApiUrl;
    }

    get isDev() {
        return process.env.ENV_CONFIGURATION === 'dev';
    }
    get isDemo() {
        return process.env.ENV_CONFIGURATION === 'demo';
    }
}

export default Config.getInstance();
