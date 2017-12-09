namespace DotNetGet

open DotNetGet.Get
open System.IO

module Program = 

    [<EntryPoint>]
    let main argv =
        match argv |> Array.toList with
        | [repo; path] -> 
            let release = getLatestRelease repo
            match Directory.Exists path with
            | true ->
                downloadAssets path (release.Assets |> Seq.toList)
                |> List.iter (printfn "%s")
            | false ->
                downloadAsset path (release.Assets |> Seq.head)
                |> printfn "%s"
        | _ ->
            printfn "-- invalid argument"
        0 // return an integer exit code
