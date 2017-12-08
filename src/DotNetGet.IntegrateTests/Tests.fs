module Tests

open System
open Xunit
open DotNetGet.Get

[<Fact>]
let shouldGetRelease() = 
    let release = getLatestRelease "wk-j/dotnet-filter"
    let rs = release.TagName 
    Assert.Equal(rs, "0.2.0")

[<Fact>]
let shouldDownloadAssets() = 
    let release = getLatestRelease "wk-j/dotnet-filter"
    let assets = release.Assets |> Seq.toList
    
    Assert.Equal(1, assets.Length)

    let rs = downloadAssets assets
    Assert.Equal(1, rs.Length)

[<Fact>]
let ``My test`` () =
    Assert.True(true)
