module FSync.DeleteExtraFiles

open FSync.Helpers

let deleteExtraFiles (targetDirPath: string) (sourceDirPath: string) =
    match getFilePaths targetDirPath with
    | Ok paths ->
        paths
        |> Array.Parallel.iter (fun path ->
            match fileExists (convertTargetPathToSourcePath path targetDirPath sourceDirPath) with
            | Ok true -> ()
            | Ok false ->
                match deleteFile path with
                | Ok _ -> ()
                | Error err -> printError err
            | Error err -> printError err)
    | Error err -> printError err 