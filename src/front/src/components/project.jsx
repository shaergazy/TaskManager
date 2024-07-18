import { Box, Heading, Text, Button, VStack, Flex, Center } from "@chakra-ui/react";
import moment from "moment/moment";
import { Link } from "react-router-dom";

export default function ProjectCard({ project, onDelete, onEdit, onAddEmployee }) {
  // if (!project) {
  //   return null; 
  // }
  const { id, name,directorName, customerCompanyName, contractorCompanyName, startDateTime, endDateTime, priority } = project;

  return (
    <Center> 
      <Box borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md">
      <VStack align="stretch" spacing={4}>
        <Heading as="h3" size="md">{name}</Heading>
        <Text>Director: {directorName}</Text>
        <Text>Customer Company: {customerCompanyName}</Text>
        <Text>Contractor Company: {contractorCompanyName}</Text>
        <Text>Start Date: {moment(startDateTime).format("DD/MM/YYYY")}</Text>
        <Text>End Date: {moment(endDateTime).format("DD/MM/YYYY")}</Text>
        <Text>Priority: {priority}</Text>
        <Flex justify="space-between" mt={4}>
          <Button colorScheme="red" width={'100%'} onClick={() => onDelete(id)}>Delete</Button>
          <Button colorScheme="blue" width={'100%'}  onClick={() => onEdit(project)}>Edit</Button>
        </Flex>
        <Flex>
          <Link to={`/projects/${id}/tasks`}  style={{ textDecoration: 'none', width: '100%' }}>
        <Button colorScheme="green" width={'100%'} >Tasks</Button>
      </Link>
      </Flex>
      <Flex justify={"space-between"}>
      <Link to={`/projects/${id}/employees`}  style={{ textDecoration: 'none', width: '100%' }}>
        <Button colorScheme="green" width={'100%'} >Employees</Button>
      </Link>
      </Flex>
        

      </VStack>
    </Box>
    
  </Center>

  );
}