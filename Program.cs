//Angel Armando Alvarez Izquierdo
//Ingeniería en software y minería de datos
//Examen 2do Parcial
//30 Mar 2025

using System;
using System.Collections.Generic;

class TreeNode
{
    public string Name { get; set; }
    public List<TreeNode> Children { get; set; }

    public TreeNode(string name)
    {
        Name = name;
        Children = new List<TreeNode>();
    }

    public void AddChild(TreeNode child)
    {
        Children.Add(child);
    }
}

class FamilyTree
{
    private TreeNode root;

    public FamilyTree(string rootName)
    {
        root = new TreeNode(rootName);
    }

    public TreeNode FindMember(TreeNode node, string name)
    {
        if (node == null) return null;

        if (node.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            return node;

        foreach (var child in node.Children)
        {
            TreeNode result = FindMember(child, name);
            if (result != null)
                return result;
        }

        return null;
    }
    public void AddMember(string childName, string parentName)
    {
        TreeNode parentNode = FindMember(root, parentName);

        if (parentNode != null)
        {

            TreeNode existingChild = FindMember(root, childName);
            if (existingChild == null)
            {
                existingChild = new TreeNode(childName);
            }

            parentNode.AddChild(existingChild);
            Console.WriteLine($"{childName} ha sido agregado como hijo de {parentName}.");
        }
        else
        {
            Console.WriteLine($"No se encontró el nodo con el nombre {parentName}.");
        }
    }

    public void PreOrderTraversal(TreeNode node)
    {
        if (node == null) return;

        Console.WriteLine(node.Name);

        foreach (var child in node.Children)
        {
            PreOrderTraversal(child);
        }
    }
    public void DisplayParents(string name)
    {
        TreeNode member = FindMember(root, name);
        if (member != null && member.Children.Count > 0)
        {
            Console.WriteLine($"Hijos de {name}:");
            foreach (var child in member.Children)
            {
                Console.WriteLine(child.Name);
            }
        }
        else if (member != null)
        {
            Console.WriteLine($"{name} no tiene hijos registrados.");
        }
        else
        {
            Console.WriteLine($"No se encontró el miembro con el nombre {name}.");
        }
    }

    public void StartPreOrderTraversal()
    {
        PreOrderTraversal(root);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Bienvenido a la gestión del árbol genealógico.");
        Console.Write("Ingresa el nombre de la raíz del árbol: ");
        string rootName = Console.ReadLine();
        FamilyTree familyTree = new FamilyTree(rootName);

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Agregar miembro");
            Console.WriteLine("2. Recorrer el árbol (Preorden)");
            Console.WriteLine("3. Encontrar los hijos de un miembro");
            Console.WriteLine("4. Salir");
            Console.Write("Selecciona una opción: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Ingresa el nombre del hijo: ");
                    string childName = Console.ReadLine();
                    Console.Write("Ingresa el nombre del padre/madre: ");
                    string parentName = Console.ReadLine();
                    familyTree.AddMember(childName, parentName);
                    break;

                case "2":
                    Console.WriteLine("Recorrido Preorden:");
                    familyTree.StartPreOrderTraversal();
                    break;

                case "3":
                    Console.Write("Ingresa el nombre del miembro para buscar sus hijos: ");
                    string memberName = Console.ReadLine();
                    familyTree.DisplayParents(memberName);
                    break;

                case "4":
                    Console.WriteLine("Saliendo del programa. ¡Adiós!");
                    running = false;
                    break;

                default:
                    Console.WriteLine("Opción no válida, intenta nuevamente.");
                    break;
            }
        }
    }
}