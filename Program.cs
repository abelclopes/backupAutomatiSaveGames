using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;

namespace copy_backup
{
    class Program
    {
        static void Main(string[] args)
        {
            string saveAtual = null;
            string copiaNovoSave = null;
            double tempoBackup = 50;
            
            Console.WriteLine("======================================================================");
            Console.WriteLine("======================================================================");
            
            Console.WriteLine("ATENÇÂO VOCÊ DEVE DELETAR OS BACKUP MAIS ANTIGOS MANUALMENTE!!!!!!!!!!");

            Console.WriteLine("======================================================================");
            Console.WriteLine("======================================================================");

            Console.WriteLine("informe o de quanto em quanto tempo deseja realizar que deseja fazer backup ");
            Console.WriteLine("(exemplo 10 é 10 segundos um minuto é 60, 2 min 120 e assim por diante)");
            tempoBackup = double.Parse(Console.ReadLine());
            while(tempoBackup < 10)
            { 
                Console.WriteLine("======================================================================");
                Console.WriteLine("não aceita menos que 10 segundos, gera muitos beckups");
                tempoBackup = double.Parse(Console.ReadLine());
            }
            var observable = Observable.Interval(TimeSpan.FromSeconds(tempoBackup)).Do((x) => { }) ; // log events

            Console.WriteLine("======================================================================");
            Console.WriteLine("informe o diretorio do save game que deseja fazer backup ex: c:\\Program Files (x86)\\Ubisoft\\Ubisoft Game Launcher\\savegames");
            saveAtual = Console.ReadLine();
            if(saveAtual == null || saveAtual == ""){
                saveAtual = "C:\\Program Files (x86)\\Ubisoft\\Ubisoft Game Launcher\\savegames";
            } 
            Console.WriteLine("======================================================================");
            Console.WriteLine($"Diretorio infromado para copia foi {saveAtual}");
            
            if (saveAtual != null) {
                Console.WriteLine("informe o diretorio para fazer backup ou apenas de enter o deretorio padrão é C:\\backup_save_ghost");
                copiaNovoSave = Console.ReadLine();
                if(copiaNovoSave == null || copiaNovoSave == ""){
                   copiaNovoSave = "C:\\backup_save_ghost\\";
                }    
            }
            Console.WriteLine("======================================================================");
            Console.WriteLine($"Diretorio infromado para salvar a copia foi {copiaNovoSave}");
            Console.WriteLine("======================================================================");
            Console.WriteLine("Deseja realmente iniciar o backp Y/N");
            string yn = Console.ReadLine();
            Console.WriteLine("======================================================================");
            if(yn.ToLower() == "y") {
                //primeiro subscriber
                observable.Subscribe((x) =>
                {
                    Console.WriteLine(".....................................");
                    Console.WriteLine($"backup realizado as {DateTime.Now}");                    
                    SistemaBackup.Backup(saveAtual, copiaNovoSave + "backup-"+ DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
                });
            }else{
                Console.WriteLine("======================================================================");
                Console.WriteLine("Precione uma tecla para encerrar o programa");
            }
            Console.ReadLine();
        }        
    }
}
