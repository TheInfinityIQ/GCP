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

export type SteamAppsResponse = {
    steamApps: [
        {
            id: number;
            name: string;
        }
    ];
};

export type ParseVDFResponse = {
    steamAppNames: [
        {
          appId: number,
          appName: string
        }
      ]
}

export type SteamAppResponse = {
    type: string,
    name: string,
    steam_appid: number,
    required_age: string,
    is_free: boolean,
    dlc: [
      number
    ],
    detailed_description: string,
    about_the_game: string,
    short_description: string,
    supported_languages: string,
    header_image: string,
    website: string,
    legal_notice: string,
    developers: [
      string
    ],
    publishers: [
      string
    ],
    price_overview: {
      currency: string,
      initial: number,
      final: number,
      discount_percent: number,
      initial_formatted: string,
      final_formatted: string
    },
    platforms: {
      windows: boolean,
      mac: boolean,
      linux: boolean
    },
    metacritic: {
      score: number,
      url: string
    },
    categories: [
      {
        id: number,
        description: string
      }
    ],
    genres: [
      {
        id: string,
        description: string
      }
    ],
    screenshots: [
      {
        id: number,
        path_thumbnail: string,
        path_full: string
      }
    ],
    movies: [
      {
        id: number,
        name: string,
        thumbnail: string,
        webm: {
          number: string,
          max: string
        },
        mp4: {
          number: string,
          max: string
        },
        highlight: boolean
      }
    ],
    release_date: {
      coming_soon: boolean,
      date: string
    },
    background: string,
    background_raw: string
}
