export type TokenResponse = {
    accessToken: string;
    expiriesOn: Date;
};

export type SecretResponse = {
    value: string;
};


const a: SecretResponse = {
    value : "string"
} 