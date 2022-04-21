export type TokenResponse = {
    accessToken: string;
    expiriesOn: Date;
};

export type SecretResponse = {
    value: string;
};

//All of users game list
export type GameListsResponse = {
    gameList: [
        {
            owner: {
                id: number;
                displayName: string;
            };
            title: string;
            description: string;
            voteOncePerGame: boolean;
            isPublic: boolean;
            userLimit: 0;
            createdOnUtc: Date;
            lastUpdatedOnUtc: Date;
            users: [
                {
                    id: number;
                    displayName: string;
                }
            ];
        }
    ];
};

//Specific game list
export type GameListResponse = {
    id: number;
    owner: {
        id: number;
        displayName: string;
    };
    title: string;
    description: string;
    voteOncePerGame: boolean;
    isPublic: boolean;
    userLimit: number;
    createdOnUtc: Date;
    lastUpdatedOnUtc: Date;
    users: [
        {
            id: number;
            displayName: string;
        }
    ];
};

export type SteamResponse = {
    steamApps: [
        {
            id: 0;
            name: string;
        }
    ];
};
