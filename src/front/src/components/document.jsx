import React from 'react';
import { Box, VStack, Heading, Text, Center, Flex, Button } from '@chakra-ui/react';
import { Link } from 'react-router-dom';

const DocumentCard = ({ document, onDelete }) => {
    const {id, name, path} = document;
  return (
    <Center>
      <Box borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md" _hover={{ bg: 'gray.100' }}>
        <Flex justify="space-between" align="center">
          <Link to={path} target="_blank" style={{ textDecoration: 'none', flex: 1 }}>
            <Heading as="h3" size="md">{name}</Heading>
          </Link>
          <Button colorScheme="red" ml={4} onClick={() => onDelete(id)}>
            Delete
          </Button>
        </Flex>
        <VStack align="stretch" spacing={4} mt={4}>
        </VStack>
      </Box>
    </Center>
  );
};

export default DocumentCard;
