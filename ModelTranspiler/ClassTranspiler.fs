﻿module ClassTranspiler

open System
open Microsoft.CodeAnalysis.CSharp
open Microsoft.CodeAnalysis
open Microsoft.CodeAnalysis.CSharp.Syntax
open System.Reflection
open System.Linq.Expressions

let convertType csharpType =
    csharpType

let convertProperty (propertyDeclaration: PropertyDeclarationSyntax) = 
    let propertyType = convertType (propertyDeclaration.Type.ToString()) in
    let propertyName = propertyDeclaration.Identifier.ToString() in
    let fieldDecl = sprintf "private _%s : %s;\n" propertyName propertyType in

    let accessors = Seq.cast<AccessorDeclarationSyntax> 
                        (Seq.filter (fun i -> TypeExtensions.IsInstanceOfType(typeof<AccessorDeclarationSyntax>, i))
                           (Seq.cast<SyntaxNode> (propertyDeclaration.DescendantNodes())))


    let convertSimpleGetter (accessor: AccessorDeclarationSyntax) = 
            sprintf "get %s() : %s\n{\nreturn this._%s;\n}\n" propertyName propertyType propertyName
    in

    let convertSimpleSetter (accessor: AccessorDeclarationSyntax) =
            sprintf "set %s(new%s : %s)\n{\nthis._%s = new%s;\n}\n"
                propertyName propertyName propertyType propertyName propertyName
    in

     
    let convertAccessor (accessor: AccessorDeclarationSyntax) =
        match (accessor.ToString()) with
            | "get;" -> convertSimpleGetter accessor
            | "public get;" -> convertSimpleGetter accessor
            | "set;" -> convertSimpleSetter accessor
            | "public set;" -> convertSimpleSetter accessor
            | _ -> ""
    in
    Seq.fold (+) fieldDecl (Seq.map convertAccessor accessors)

let createConstructor (properties: seq<PropertyDeclarationSyntax>) =
    let propertySetters = Seq.map (fun (prop: PropertyDeclarationSyntax) ->
                                        let identifier =  (prop.Identifier.ToString()) in
                                        "if (jsonData." + identifier + ") {\n" +
                                        "this._" + identifier + " = " + identifier + ";\n}\n") properties
    in "constructor (jsonData) {\n" + (Seq.fold (+) "" propertySetters) + "}\n"

let convertClass (classDeclaration : ClassDeclarationSyntax) = 
    let properties = Seq.cast<PropertyDeclarationSyntax> (
                         Seq.filter (fun i -> TypeExtensions.IsInstanceOfType(typeof<PropertyDeclarationSyntax>, i))
                             (Seq.cast<SyntaxNode> (classDeclaration.DescendantNodes())))
    in
    let constructor = createConstructor properties in
    let convertedProperties = Seq.fold (+) "" (Seq.map (fun prop -> (convertProperty prop) + "\n") properties) in
    "class " + classDeclaration.Identifier.ToString() + " {\n" + constructor + convertedProperties + "}"

