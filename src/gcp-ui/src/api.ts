import { test } from "./models";

const path: string = "https://localhost:5001";

// Log in
const GetLogin = await fetch(path + "/token", {
    method: "POST",
    headers: {
        "Content-Type": "application/json",
    },
    body: JSON.stringify({
        email: "mod@gcp.com",
        password: "Password-1",
    }),
}).then((response: Response) => {
    return response.json();
});

export { GetLogin };
