module FSync.Helpers

open System
open System.IO

let printError (message: string) =
    printfn $"ERROR: {message}"
    
let printWarning (message: string) =
    printfn $"WARNING: {message}"

let getDirPaths (path: string) =
    try
        Ok (Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories))
    with
    | :? UnauthorizedAccessException as ex ->
        Error $"Insufficient permissions when accessing one or more directories for '{path}'.\n'{ex.Message}'"
    | ex -> Error $"An unknown error occurred when accessing one or more directories for '{path}'.\n{ex.Message}"

let dirExists (path: string) =
    try
        Ok (Directory.Exists(path))
    with
    | ex -> Error $"An unknown error occurred when checking the existence of the directory '{path}'.\n{ex.Message}"
    
let deleteDir (path: string) =
    try
        Ok (Directory.Delete(path, true))
    with
    | :? DirectoryNotFoundException -> Ok ()
    | :? UnauthorizedAccessException -> Error $"Insufficient permissions to the delete the directory '{path}'"
    | ex -> Error $"An unknown error occurred when deleting the directory '{path}'.\n{ex.Message}"
    
let getFilePaths (path: string) =
    try
        Ok (Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
    with
    | :? UnauthorizedAccessException as ex ->
        Error $"Insufficient permissions when accessing one or more directories for '{path}'.\n'{ex.Message}'"
    | ex -> Error $"An unknown error occurred when accessing one or more directories for '{path}'.\n{ex.Message}"
    
let fileExists (path: string) =
    try
        Ok (File.Exists(path))
    with
    | ex -> Error $"An unknown error occurred when checking the existence of the file '{path}'.\n{ex.Message}"
    
let deleteFile (path: string) =
    try
        Ok (File.Delete(path))
    with
    | :? UnauthorizedAccessException -> Error $"Insufficient permissions to the delete the file '{path}'"
    | ex -> Error $"An unknown error occurred when deleting the file '{path}'.\n{ex.Message}"
    
let createDir (path: string) =
    try
        Ok (Directory.CreateDirectory(path) |> ignore)
    with
    | :? UnauthorizedAccessException -> Error $"Insufficient permissions to create the directory '{path}'"
    | ex -> Error $"An unknown error occurred when creating the directory '{path}'.\n{ex.Message}"
    
let copyFile (sourcePath: string) (targetPath: string) =
    try
        Ok (File.Copy(sourcePath, targetPath))
    with
    | :? UnauthorizedAccessException ->
        Error $"Insufficient permissions to copy the file from '{sourcePath}' to '{targetPath}'"
    | ex ->
        Error $"An unknown error occurred when copying the file from '{sourcePath}' to '{targetPath}'.\n{ex.Message}"
    
let isFileSizeSame (sourcePath: string) (targetPath: string) =
    FileInfo(sourcePath).Length = FileInfo(targetPath).Length
    
let replaceFile (sourcePath: string) (targetPath: string) =
    try
        Ok (File.Copy(sourcePath, targetPath, true))
    with
    | :? UnauthorizedAccessException ->
        Error $"Insufficient permissions to replace the file '{targetPath}' with '{sourcePath}'"
    | ex -> Error $"An unknown error occurred when replacing the file '{targetPath}' with '{sourcePath}'.\n{ex.Message}"
    
let isFileContentSame (sourcePath: string) (targetPath: string) =
    try
        use sourceFile = File.OpenRead(sourcePath)
        use targetFile = File.OpenRead(targetPath)
        let mutable sourceFileByte = sourceFile.ReadByte()
        let mutable targetFileByte = targetFile.ReadByte()
        let mutable fileBytesSame = true
        while sourceFileByte <> -1 && targetFileByte <> -1 && fileBytesSame do
            if sourceFileByte = targetFileByte then
                sourceFileByte <- sourceFile.ReadByte()
                targetFileByte <- targetFile.ReadByte()
            else
                fileBytesSame <- false
        Ok fileBytesSame
    with
    | :? UnauthorizedAccessException ->
        Error $"Insufficient permissions to read one or both of the files '{sourcePath}', '{targetPath}'"
    | ex ->
        Error $"An unknown error occurred when reading one of both of the files '{sourcePath}', '{targetPath}'.\n{ex.Message}"
    
let convertTargetPathToSourcePath (path: string) (targetDirPath: string) (sourceDirPath: string) =
    path.Replace(targetDirPath, sourceDirPath)
    
let convertSourcePathToTargetPath (path: string) (sourceDirPath: string) (targetDirPath: string) =
    path.Replace(sourceDirPath, targetDirPath)