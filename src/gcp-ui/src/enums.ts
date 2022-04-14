enum BgType {
    Default,
    NoBackgroundPicture = 1 << 1,
    NoBlurBackground = 1 << 2,
    NoBackgrounds = NoBackgroundPicture | NoBlurBackground,
}




export { BgType };