module FSync.CopyFiles

open FSync.Helpers

let copyMissingFiles (sourceDirPath: string) (targetDirPath: string) =
    match getFilePaths sourceDirPath with
    | Ok paths ->
        paths
        |> Array.Parallel.iter (fun path ->
            let targetPath = convertSourcePathToTargetPath path sourceDirPath targetDirPath
            match fileExists targetPath with
            | Ok true ->
                match isFileSizeSame path targetPath with
                | true ->
                    match isFileContentSame path targetPath with
                    | Ok true -> ()
                    | Ok false ->
                        match replaceFile path targetPath with
                        | Ok _ -> ()
                        | Error err -> printError err
                    | Error err -> printError err
                | false ->
                    match replaceFile path targetPath with
                    | Ok _ -> ()
                    | Error err -> printError err
            | Ok false ->
                match copyFile path targetPath with
                | Ok _ -> ()
                | Error err -> printError err
            | Error err -> printError err)
    | Error err -> printError err