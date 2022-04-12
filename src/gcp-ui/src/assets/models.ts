

type User = {
    displayName: string;
}

type Game = {
    id: number;
    name: string;
    normalizedName: string;
    //MetaData
    steamAppId: string;
    releaseDate: Date;
}

type Role = {
    
}

export {} // Used to tell ts that it's not a legacy file