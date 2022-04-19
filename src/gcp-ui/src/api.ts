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

    public async SendDELETRequestAsync(uri: string, headers?: HeadersInit): Promise<Response> {
        return this.SendRequestAsync(uri, undefined, HttpMethods.DELETE, headers);
    }

    public async SendRequestAsync(uri: string, body?: object, method?: HttpMethods, headers?: HeadersInit): Promise<Response> {
        const response = await fetch(`${this._path}/${uri}`, {
            method: method ?? HttpMethods.POST,
            headers: headers,
            body: body ? JSON.stringify(body) : undefined,
        });

        return response;
    }
}

export class Api extends BaseApi {
    constructor() {
        super();
    }

    async GetLoginAsync(email: string, password: string) {
        const uri = "token";
        const body = {
            email: email,
            password: password,
        };
        const headers = {
            "Content-Type": "application/json",
        };

        const tokenResponse = await this.SendPOSTRequestAsync(uri, body, headers);

        const tokenJsonResponse = await tokenResponse.json();
        localStorage.setItem("login", tokenJsonResponse);
    }
}
