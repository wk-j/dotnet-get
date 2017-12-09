module DotNetGet.Get

open Hopac
open HttpFs.Client
open DotNetGet.Types
open HttpFs.Client.Request
open System.IO

let getLatestRelease repo = 
    sprintf "https://api.github.com/repos/%s/releases/latest" repo
    |> createUrl Get
    |> setHeader (UserAgent "Chrome or summat")
    |> responseAsString
    |> run
    |> Release.Parse

let private extractName url = 
    let uri = System.Uri url
    Path.GetFileName (uri.LocalPath)


let downloadAsset savePath (asset: Release.Asset) = 
    job { 
        use! res = 
            createUrl Get (asset.BrowserDownloadUrl)
            |> setHeader (UserAgent "Chrome or summat")
            |> getResponse
        use fileStream = new FileStream(savePath, FileMode.Create)
        do! res.body.CopyToAsync fileStream |> Job.awaitUnitTask
        return savePath
    }
    |> run

let downloadAssets dir (assets: Release.Asset list) = 
    let download dir (assets: Release.Asset) = 
        let name = extractName (assets.BrowserDownloadUrl)
        let path = Path.Combine(dir, name)
        downloadAsset path assets
    assets |> List.map (download dir)