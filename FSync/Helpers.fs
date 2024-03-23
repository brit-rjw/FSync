module FSync.Helpers

open System
open System.IO

let getDirPaths (path: string) =
    Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories)

let dirExists (path: string) =
    Directory.Exists(path)
    
let deleteDir (path: string) =
    Directory.Delete(path, true)
    
let getFilePaths (path: string) =
    Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
    
let fileExists (path: string) =
    File.Exists(path)
    
let deleteFile (path: string) =
    File.Delete(path)
    
let createDir (path: string) =
    Directory.CreateDirectory(path) |> ignore
    
let copyFile (sourcePath: string) (targetPath: string) =
    File.Copy(sourcePath, targetPath)
    
let isFileSizeSame (sourcePath: string) (targetPath: string) =
    FileInfo(sourcePath).Length = FileInfo(targetPath).Length
    
let replaceFile (sourcePath: string) (targetPath: string) =
    File.Copy(sourcePath, targetPath, true)
    
let isFileContentSame (sourcePath: string) (targetPath: string) =
    let sourceFile = File.OpenRead(sourcePath)
    let targetFile = File.OpenRead(targetPath)
    let mutable sourceFileByte = sourceFile.ReadByte()
    let mutable targetFileByte = targetFile.ReadByte()
    let mutable fileBytesSame = true
    while sourceFileByte <> -1 && targetFileByte <> -1 && fileBytesSame do
        if sourceFileByte = targetFileByte then
            sourceFileByte <- sourceFile.ReadByte()
            targetFileByte <- targetFile.ReadByte()
        else
            fileBytesSame <- false
    sourceFile.Close()
    targetFile.Close()
    fileBytesSame
    
let convertTargetPathToSourcePath (path: string) (targetDirPath: string) (sourceDirPath: string) =
    path.Replace(targetDirPath, sourceDirPath)
    
let convertSourcePathToTargetPath (path: string) (sourceDirPath: string) (targetDirPath: string) =
    path.Replace(sourceDirPath, targetDirPath)
    
let printErrorToConsoleAndExit (message: string) =
    printfn $"ERROR: {message}"
    Environment.Exit(1)

let checkArgvArrayCount (args: Array) =
    match args.Length < 2 with
    | true -> ()
    | false -> printErrorToConsoleAndExit "Insufficient number of arguments"

let checkArgvDirPaths (args: string array) =
    args
    |> Array.iter (fun path ->
        match dirExists path with
        | true -> ()
        | false -> printErrorToConsoleAndExit $"Cannot find the directory '{path}'")