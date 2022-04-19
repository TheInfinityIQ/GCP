import { TokenResponse, SecretResponse } from "./models";

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

    public async GetSecretAsync(): Promise<SecretResponse> {
        const uri: string = "api/Secret";

        const token: TokenResponse | undefined = this.GetCachedAccessToken();
        if (!token) throw new Error("API: Not authenticated");

        const headers: HeadersInit = { Authorization: `Bearer ${token.accessToken}` };

        const secretResponse: Response = await this.SendGETRequestAsync(uri, headers);
        const secretJsonResponse: SecretResponse = await secretResponse.json();

        return secretJsonResponse;
    }
}
