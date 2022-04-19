import { TokenResponse } from "./models";

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
            const response = await fetch(`${this._path}/${uri}`, {
                method: method ?? HttpMethods.POST,
                headers: headers,
                body: body ? JSON.stringify(body) : undefined,
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
        const headers: HeadersInit = {
            "Content-Type": "application/json",
        };

        const tokenResponse: Response = await this.SendPOSTRequestAsync(uri, body, headers);
        const tokenJsonResponse: TokenResponse = await tokenResponse.json();

        localStorage.setItem("login", JSON.stringify(tokenJsonResponse));

        return tokenJsonResponse;
    }
}
