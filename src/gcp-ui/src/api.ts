import { TokenResponse, SecretResponse, GameListResponse, GameListsResponse, SteamAppsResponse, SteamAppResponse, SteamAddAppsResponse } from "./models";

const enum HttpMethods {
    GET = "GET",
    POST = "POST",
    PUT = "PUT",
    DELETE = "DELETE",
}

class BaseApi {
    private _path: string = "https://localhost:5001";

    public async SendGETRequestAsync(uri: string, headers?: HeadersInit): Promise<Response> {
        return this.SendRequestAsync(uri, undefined, HttpMethods.GET, headers);
    }

    public async SendPOSTRequestAsync(uri: string, body: object, headers?: HeadersInit): Promise<Response> {
        return this.SendRequestAsync(uri, body, HttpMethods.POST, headers);
    }

    public async SendPUTRequestAsync(uri: string, body: object, headers?: HeadersInit): Promise<Response> {
        return this.SendRequestAsync(uri, body, HttpMethods.PUT, headers);
    }

    public async SendDELETERequestAsync(uri: string, headers?: HeadersInit): Promise<Response> {
        return this.SendRequestAsync(uri, undefined, HttpMethods.DELETE, headers);
    }

    public async SendRequestAsync(uri: string, body?: object, method?: HttpMethods, headers?: HeadersInit): Promise<Response> {
        try {
            const isBodyHere: Boolean = !!body;
            const myMethod: HttpMethods = method ?? HttpMethods.POST;
            const myHeaders: HeadersInit = isBodyHere ? { "Content-Type": "application/json", ...headers } : { ...headers };
            const myBody: BodyInit | undefined = isBodyHere ? JSON.stringify(body) : undefined;

            const response = await fetch(`${this._path}/${uri}`, {
                method: myMethod,
                headers: myHeaders,
                body: myBody,
            });

            // TODO: Update to use a callback to handle "non-2.." status codes
            // or use a custom error class that stores the resposne and have the caller read response for errors...
            if (!response?.ok) {
                throw new Error(`[${response.status}] response not ok`);
            }

            return response;
        } catch (error) {
            console.log("API EXCEPTION:", error);
            throw error;
        }
    }

    public IsAuthenticated(): Boolean {
        return !!localStorage.getItem("login");
    }

    public GetCachedAccessToken(): TokenResponse | undefined {
        if (this.IsAuthenticated()) {
            return JSON.parse(localStorage.getItem("login") ?? "{}");
        } else {
            return undefined;
        }
    }

    public SetCachedAccessToken(token: TokenResponse) {
        localStorage.setItem("login", JSON.stringify(token));
    }
}

export class Api extends BaseApi {
    constructor() {
        super();
    }

    //Testing the API. Not useful
    public async GetSecretAsync(): Promise<SecretResponse> {
        const uri: string = "api/Secret";

        const token: TokenResponse | undefined = this.GetCachedAccessToken();
        if (!token) throw new Error("API: Not authenticated");

        const headers: HeadersInit = { Authorization: `Bearer ${token.accessToken}` };

        const secretResponse: Response = await this.SendGETRequestAsync(uri, headers);
        const secretJsonResponse: SecretResponse = await secretResponse.json();

        return secretJsonResponse;
    }

    //Untested
    public async GetPublicSecretAsync(): Promise<SecretResponse> {
        const uri = "api/secret/public";

        const secretResponse = await this.SendGETRequestAsync(uri);
        const secretJsonResponse: SecretResponse = await secretResponse.json();

        return secretJsonResponse;
    }

    //Auth
    public async GetLoginAsync(email: string, password: string): Promise<TokenResponse> {
        const uri: string = "token";
        const body: object = {
            email: email,
            password: password,
        };

        const tokenResponse: Response = await this.SendPOSTRequestAsync(uri, body);
        const tokenJsonResponse: TokenResponse = await tokenResponse.json();

        //put at call site SoC
        this.SetCachedAccessToken(tokenJsonResponse);

        return tokenJsonResponse;
    }

    //UNTESTED BELOW THIS COMMENT
    public async RegisterAccount(email: string, displayName: string, password: string): Promise<undefined | Error> {
        const uri = "account/register";
        const body: object = {
            email: email,
            displayName: displayName,
            password: password,
        };

        //TODO: think about how to handle unsuccessful registration attempt
        const registerResponse: Response = await this.SendPOSTRequestAsync(uri, body);
        if (!registerResponse.ok) {
            return;
        }

        return new Error("API: Registration failure");
    }

    public async GetUsersGameLists(hasDiscord: boolean, activeFrom: Date): Promise<GameListsResponse> {
        const queryParams = `?hasDiscord=${hasDiscord}&activeFrom=${activeFrom}`;
        const uri = "api/gamelist" + queryParams;

        const gameListResponse: Response = await this.SendGETRequestAsync(uri);
        const gameListJsonResponse: GameListsResponse = await gameListResponse.json();

        return gameListJsonResponse;
    }

    public async CreateGameList(title: string, description: string, voteOncePerGame: boolean, isPublic: boolean = false, userLimit: number = 9999): Promise<GameListResponse> {
        const uri = "api/gamelist";
        const body = {
            title: title,
            description: description,
            voteOncePerGame: voteOncePerGame,
            isPublic: isPublic,
            userLimit: userLimit,
        };

        const gameListResponse: Response = await this.SendPOSTRequestAsync(uri, body);
        const gameListJsonResponse: GameListResponse = await gameListResponse.json();

        return gameListJsonResponse;
    }

    public async GetGameList(id: number): Promise<GameListResponse> {
        const uri = `api/gamelist/${id}`;

        const gameListResponse: Response = await this.SendGETRequestAsync(uri);
        const gameListJsonResponse: GameListResponse = await gameListResponse.json();

        return gameListJsonResponse;
    }

    public async UpdateGameList(id: number, title: string, description: string, voteOncePerGame: boolean, isPublic: boolean, userLimit: number): Promise<GameListsResponse> {
        const uri = `api/gamelist/${id}`;
        const body = {
            title: title,
            description: description,
            voteOncePerGame: voteOncePerGame,
            isPublic: isPublic,
            userLimit: userLimit,
        };

        const gameListResponse: Response = await this.SendPUTRequestAsync(uri, body);
        const gameListJsonResponse: GameListsResponse = await gameListResponse.json();

        return gameListJsonResponse;
    }

    public async DeleteGameList(id: number): Promise<GameListResponse> {
        const uri = `api/gamelist/${id}`;

        const gameListResponse: Response = await this.SendGETRequestAsync(uri);
        const gameListJsonResponse: GameListResponse = await gameListResponse.json();

        return gameListJsonResponse;
    }

    public async GetSteamApps(): Promise<SteamAppsResponse> {
        const uri = "api/steam/app";

        const steamAppResponse = await this.SendGETRequestAsync(uri);
        const steamAppJsonResponse: SteamAppsResponse = await steamAppResponse.json();

        return steamAppJsonResponse;
    }

    public async GetSteamApp(id: number): Promise<SteamAppResponse> {
        const uri = `api/steam/app/${id}`;

        const steamAppResponse = await this.SendGETRequestAsync(uri);
        const steamAppJsonResponse: SteamAppResponse = await steamAppResponse.json();

        return steamAppJsonResponse;
    }

    //TODO: Adjust any type in promise once model is created
    public async AddSteamApps(): Promise<SteamAddAppsResponse> {
        const uri = "api/steam/parse-vdf";
        
        //DO file parsing stuff
        const body = {

        }
        
        const addSteamAppsResponse: Response = await this.SendPOSTRequestAsync(uri, body);
        const addSteamAppsJsonResponse: SteamAddAppsResponse = await addSteamAppsResponse.json();

        return addSteamAppsJsonResponse;
    }
}
