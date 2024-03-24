module FSync.CreateMissingDirs

open FSync.Helpers

let createMissingDirs (sourceDirPath: string) (targetDirPath: string) =
    match getDirPaths sourceDirPath with
    | Ok paths ->
        paths
        |> Array.iter (fun path ->
            let targetPath = convertSourcePathToTargetPath path sourceDirPath targetDirPath
            match dirExists targetPath with
            | Ok true -> ()
            | Ok false ->
                match createDir targetPath with
                | Ok _ -> ()
                | Error err -> printError err
            | Error err -> printError err)
    | Error err -> printError err