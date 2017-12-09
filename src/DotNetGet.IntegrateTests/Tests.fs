module Tests

open System
open Xunit
open DotNetGet.Get
open System.IO

let temp = "/Users/wk/Source/DotNetGet/temp"

[<Fact>]
let shouldGetRelease() = 
    let release = getLatestRelease "wk-j/dotnet-filter"
    let rs = release.TagName 
    Assert.Equal(rs, "0.2.0")

[<Fact>]
let shouldExtractName() = 
    let url = "http://www.test.com/file1.txt?a=b"
    let uri = Uri url
    let name = Path.GetFileName uri.LocalPath
    Assert.Equal("file1.txt", name)

[<Fact>]
let shouldDownloadAsset() = 
    let release = getLatestRelease "wk-j/dotnet-filter"
    let assets = release.Assets
    let path = Path.Combine(temp, "Hello.zip")
    let rs = downloadAsset path (assets |> Seq.head)
    Assert.Equal(path, rs)

[<Fact>]
let shouldDownloadAssets() = 
    let release = getLatestRelease "wk-j/dotnet-filter"
    let assets = release.Assets |> Seq.toList
    
    Assert.Equal(1, assets.Length)

    let rs = downloadAssets temp assets
    Assert.Equal(1, rs.Length)

[<Fact>]
let ``My test`` () =
    Assert.True(true)
