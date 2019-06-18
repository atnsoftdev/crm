def getChangeSet() {
    return sh(returnStdout: true, script: 'git diff-tree --no-commit-id --name-status -r HEAD').trim()
}

pipeline {
    agent any
    stages {
        stage('checkout code') {
            agent any
            steps {
                script {
                    def scmVars = checkout([
                        $class: 'GitSCM',
                        branches: [[name: 'feature/init-proj']],
                        doGenerateSubmoduleConfigurations: false,
                        extensions: [],
                        submoduleCfg: [],
                        userRemoteConfigs: [
                            [credentialsId: 'github-ssh-key', url: 'git@github.com:tungphuong/crm.git']
                        ]
                    ])
                    
                    env.GIT_COMMIT = scmVars.GIT_COMMIT   
                }
            }
        }

        stage('trigger build idm') {
            agent any
            when {
                 expression {
                    matches = sh(returnStatus:true, script: "git diff-tree --no-commit-id --name-status -r $GIT_COMMIT|grep 'src/crm.idm.web/**'")
                    return !matches
                }
            }
            steps {

                echo "Build Idm"
            }
        }
        
        stage('trigger build customer api') {
            agent any
            when {
                 expression {
                    matches = sh(returnStatus:true, script: "git diff-tree --no-commit-id --name-status -r $GIT_COMMIT|grep 'src/crm.customer.api/**'")
                    return !matches
                }
            }
            steps {

                echo "Build customer API"
            }
        }
    }
}