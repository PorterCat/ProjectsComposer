import os
import pathlib

def collect_ts_files(output_file="combined_code.txt"):
    project_root = pathlib.Path.cwd()
    combined_content = []
    
    ignored_dirs = {'node_modules', '.git', 'dist', 'build', '__pycache__'}
    
    for file_path in project_root.rglob("*"):
        if any(part in ignored_dirs for part in file_path.parts):
            continue
            
        if file_path.suffix in ('.ts', '.tsx'):
            try:
                with open(file_path, 'r', encoding='utf-8') as f:
                    content = f.read()
                    
                relative_path = file_path.relative_to(project_root)
                combined_content.append(f"// {relative_path}\n{content}\n")
                
            except Exception as e:
                print(f"Ошибка чтения файла {file_path}: {str(e)}")
    
    with open(output_file, 'w', encoding='utf-8') as f:
        f.writelines(combined_content)
    
    print(f"Файлы объединены в {output_file}")

if __name__ == "__main__":
    collect_ts_files("project-composer")