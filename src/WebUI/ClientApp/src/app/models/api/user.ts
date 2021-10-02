import { IDictionary, INDictionary } from 'app/models/dictionary';

export interface IAppState {
    isDebug: boolean;
    version: string;
    activeFeatures: IDictionary<boolean>;
}

export interface IUserSession {
    appState: IAppState;
    displayName: string;
    email: string;
    photoBase64: string;
    permissions: INDictionary<boolean>;

}