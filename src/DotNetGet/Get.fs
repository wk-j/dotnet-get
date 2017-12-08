module DotNetGet.Get

open Hopac
open HttpFs.Client
open DotNetGet.Types
open HttpFs.Client.Request
open FSharp.Data.HttpRequestHeaders
open System.IO

let getLatestRelease repo = 
    sprintf "https://api.github.com/repos/%s/releases/latest" repo
    |> createUrl Get
    |> setHeader (RequestHeader.UserAgent "Chrome or summat")
    |> responseAsString
    |> run
    |> Release.Parse

let downloadAssets (assets: Release.Asset list) = 
    let download url = 
        job { 
            use! res = 
                createUrl Get url 
                |> setHeader (RequestHeader.UserAgent "Chrome or summat")
                |> getResponse
            use fileStream = new FileStream("/Users/wk/Source/DotNetGet/temp/xyz.zip", FileMode.Create)
            do! res.body.CopyToAsync fileStream |> Job.awaitUnitTask
        }
        |> run

    assets 
    |> List.map (fun x -> x.BrowserDownloadUrl) 
    |> List.map download