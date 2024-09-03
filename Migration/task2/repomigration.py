import os
import subprocess
import shutil
import stat
from github import Github


SOURCE_TOKEN = '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'
DEST_TOKEN = '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'


if not SOURCE_TOKEN or not DEST_TOKEN:
    raise ValueError("Please set SOURCE_GITHUB_TOKEN and DEST_GITHUB_TOKEN environment variables")


source_github = Github(SOURCE_TOKEN)
dest_github = Github(DEST_TOKEN)


source_username = 'Senthamil2003'
dest_username = 'Senthamil20031'


source_user = source_github.get_user(source_username)
repos = source_user.get_repos()


CLONE_DIR = 'cloned_repos'


if not os.path.exists(CLONE_DIR):
    os.makedirs(CLONE_DIR)


repos_to_move = ["cyberadmin"]


def remove_readonly(func, path, excinfo):
    os.chmod(path, stat.S_IWRITE)
    func(path)


for repo in repos:
    try:
        repo_name = repo.name
        if repo_name in repos_to_move:
            clone_url = repo.clone_url
            print(f"Cloning repository: {repo_name}")

        
            clone_path = os.path.join(CLONE_DIR, repo_name)
            subprocess.run(['git', 'clone', '--mirror', clone_url, clone_path], check=True, stderr=subprocess.PIPE, universal_newlines=True)

            
            print(f"Creating new repository in destination account: {repo_name}")
            dest_user = dest_github.get_user()
            dest_repo = dest_user.create_repo(name=repo_name, private=repo.private)

            print(f"Pushing repository {repo_name} to destination account")
            push_url = f'https://{DEST_TOKEN}@github.com/{dest_username}/{repo_name}.git'
            
           
            try:
                subprocess.run(['git', '-C', clone_path, 'push', '--prune', '--all', push_url], check=True, stderr=subprocess.PIPE, universal_newlines=True)
                subprocess.run(['git', '-C', clone_path, 'push', '--prune', '--tags', push_url], check=True, stderr=subprocess.PIPE, universal_newlines=True)
                print(f"Repository {repo_name} copied successfully!")
            except subprocess.CalledProcessError as e:
                print(f"Failed to push repository {repo_name}: {e.stderr}")

        else:
            print(f"Skipped repository: {repo_name}")

    except Exception as e:
        print(f"Failed to process repository {repo.name}: {str(e)}")

# Clean up local cloned repositories
if os.path.exists(CLONE_DIR):
    shutil.rmtree(CLONE_DIR, onerror=remove_readonly)

print("All repositories have been processed, and temporary files have been cleaned up.")