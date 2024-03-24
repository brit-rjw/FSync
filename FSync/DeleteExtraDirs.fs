module FSync.DeleteExtraDirs

open FSync.Helpers

let deleteExtraDirs (targetDirPath: string) (sourceDirPath: string) =
    match getDirPaths targetDirPath with
    | Ok paths ->
        paths
        |> Array.iter (fun path ->
            match dirExists (convertTargetPathToSourcePath path targetDirPath sourceDirPath) with
            | Ok true -> ()
            | Ok false ->
                match deleteDir path with
                | Ok _ -> ()
                | Error err -> printError err
            | Error err -> printError err)
    | Error err -> printError err